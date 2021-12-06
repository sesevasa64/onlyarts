import React, { Component } from 'react';
import {Route, NavLink, HashRouter,
        BrowserRouter,
        Switch,
        Link
} from 'react-router-dom'

import {getCookie, setCookie, deleteCookie} from './models/forCookies';

import './OnlyArts.css'

import HeaderOA from './components/HeaderOA';
import Logo from './components/Logo';
import NavigationMenu from './components/NavigationMenu';
import TagList from './components/TagList';
import CardsContentBox from './components/CardsContentBox';
import ContentPage from './components/ContentPage';
import RegistrationForm from './components/RegistrationForm';
import {t1, t2, t3, t4} from './models/TestContentCard'
import AuthForm from './components/AuthForm';
import UserPage from './components/UserPage';
import NewPostPage from './components/NewPostPage';
import EditUserPage from './components/EditUserPage';

let host_name = "https://" + document.location.host;

const max_cont = 18;

class OnlyArts extends Component
{
  constructor(props)
  {
    super(props);
    this.state = 
    {
      content_cards: [],
      current_content: 0,
      loadContent: null,

      tags: [],

      outputLoginBox: false,
      outputRegistationForm: false,
      contentIsSelect: false,
      selectedContent: null,
	    
      isAuth: false,
      User:{
        Login: ""
      },

    }
  }

  componentWillMount() 
  {
    this.userExit = this.userExit.bind(this);
    this.checkAuth();

    this.loadPopularCards = this.loadPopularCards.bind(this);
    this.setState({
      loadContent: this.loadPopularCards,
    })
    this.loadPopularCards(0, max_cont);
    this.loadPopularTags(0, 10);
    this.addNewPost = this.addNewPost.bind(this);
    this.getUserByLogin = this.getUserByLogin.bind(this);
    this.onLikeClick = this.onLikeClick.bind(this);
    this.authFunc = this.authFunc.bind(this);
	  this.userAuthorize = this.userAuthorize.bind(this);
    this.getContentById = this.getContentById.bind(this);
    this.renderLoginBox = this.renderLoginBox.bind(this);
    this.renderRegistrationForm = this.renderRegistrationForm.bind(this);
    this.renderSelectedContent = this.renderSelectedContent.bind(this);
  }

  async loadPopularTags(from, to)
  {
    let tags = [];
    let response = await fetch(`${host_name}/api/tags/popular?min=${from}&max=${to}`)
    if(response.ok)
    {
      tags = await response.json();
      this.setState({
        tags: tags,
      })
    }
  }

  async loadPopularCards(from, to, callback)
  {
    let card;
    let answer = []; // Карточки на стринице
    let response = await fetch(`${host_name}/api/contents/popular?min=${from}&max=${to}`);
    console.log("PIDOR");
    if(response.ok)
    {
      card = await response.json();
      answer = card;
      console.log(answer);
    }
    if(callback)
    {
      callback(response.ok);
    }
    this.setState({
       content_cards: answer,
    });
  }

  async getContentById(contentId)
  {
    let response = await fetch(`${host_name}/api/contents/${contentId}`);
    let json = null;
    if(response.ok)
    {
      let json = await response.json();
    }
    return json;
  }

  async getUserByLogin(login)
  {
    fetch(`${host_name}/api/users?login=${login}`)
    .then((response)=>{
      if(response.ok){
        let user = response.json();
        user['Login'] = login;
        return user;
      }
      else{
        return 0;
      }
    })
    .then((value)=>{
      if(value)
      {
        this.setState({
          User: value,
        })
      }
    })
  }

  renderSelectedContent(content)
  {
    this.setState(
      {
        contentIsSelect: true,
        selectedContent: content,
      }
    )
  }

  renderLoginBox(output)
  {
    this.setState({
      outputLoginBox: output,
    });
  }
  renderRegistrationForm(output)
  {
    this.setState({
      outputRegistationForm: output,
    });
    this.loadPopularCards(0, max_cont);
  }
  
  checkAuth()
  {
    let authToken = getCookie("authToken");
    let login = getCookie("Login");
    if(authToken && login)
    {
      this.getUserByLogin(login)
      this.setState({
        authToken: authToken,
        isAuth: true,
        outputLoginBox: false,
      })
    }
  }

  userAuthorize(authToken, login, saveAuth)
  {
    this.setState({
      authToken: authToken,
		  isAuth: true,
		  outputLoginBox: false,
      User: {
        Login: login
      },
	  })
    if(saveAuth)
    {
      setCookie("authToken", authToken);
      setCookie("Login", login);
    }
  }

  userExit(login)
  {
    this.setState({
      isAuth: false,
      User:{
        login: ""
      }
    })
    deleteCookie("authToken");
    deleteCookie("Login");
  }
  /*
        User is var with fields: {
            Login: "",
            Password: ""
        }
  */
  authFunc(User, rememberAuth)
  {
        fetch(`${host_name}/api/users/auth`,{
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(User)
        })
        .then((response) => {
            if(response.ok){
                this.getUserByLogin(User.Login);
                return response.json()
            }
            else{
                return 0;
            }
        })
        .then((result) => 
        {
            if(result)
            {
              this.userAuthorize(result.authToken, User.Login, rememberAuth)
            }
        });
    }

    /*
    Content = {
      "Name": Name1
      "Description": Description1,
      "ContentType": ContentType1,
      "LinkToPreview": LinkToPreview1,
      "LinkToBlur": LinkToBlur1,
      "UserID": 1,
      "SubTypeID": 1,
      "Images": [
        base64,
        base64,
        ...
      ]
    }
    */
  addNewPost(Content, callback_func)
  {
    let json_to_post = {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json;charset=utf-8'
      },
      body: JSON.stringify(Content)
    }
    console.log(Content);
    fetch(`${host_name}/api/contents`, json_to_post).
    then((response) =>{
      console.log(response.ok)
      if(response.ok)
      {
        callback_func(true);
        return true;
      }
      else
      {
        callback_func(false);
        return true;
      }
    }).then((value)=>{
      return false;
    })
  }

  async onLikeClick(content_id)
  {
    let response = await fetch(`${host_name}/api/content/${content_id}/like`,{
      method: 'PATCH',
      headers: {
        'Content-Type': 'application/json',
        'API-key': this.state.authToken
      } 
    });
    if(response.ok)
    {
      console.log("Like");
    }
  }
  render () {
    return (
      <div className="main-box">
          {(this.state.outputLoginBox && !this.state.isAuth) ? <AuthForm authFunc={this.authFunc} closeBox={this.renderLoginBox} reg_onClick = {this.renderRegistrationForm}/> : "" }
          {!this.state.outputRegistationForm || <RegistrationForm authFunc={this.authFunc} close_onClick={this.renderRegistrationForm}/>}
          <div className="top-flex-div">
            <Logo/>
            <HeaderOA isAuth={this.state.isAuth} User={this.state.User} onLoginClick={this.renderLoginBox} onExitClick={this.userExit}/>
          </div>
          <div className="menu-flex-div">
            <NavigationMenu User={this.state.User} isAuth={this.state.isAuth}/>
          </div>
          <div className="center-flex-div">
            <TagList tags={this.state.tags}/>
            <Switch>
              <Route path={'/ContentPage/:contentId'}> 
                <ContentPage onLikeClick={this.onLikeClick}/>
              </Route>
              <Route path={'/UserPage/:login'}>
                <UserPage content={<CardsContentBox
                                              content_onClick={this.renderSelectedContent}
                                              content={this.state.content_cards}
                                              title={`Карточки пользователя`}/>}/>
              </Route>
              <Route exact path="/" render={()=><CardsContentBox content_onClick={this.renderSelectedContent}
                                              loadContent={this.state.loadContent}
                                              content={this.state.content_cards}
                                              title={"Главная страница"}/>}/>
              <Route path="/NewPost/" render={() => <NewPostPage User={this.state.User} addNewPost={this.addNewPost}/>}/>
              <Route path="/EditUser/" render={() => <EditUserPage User={this.state.User}></EditUserPage>}></Route>
            </Switch>
          </div>
      </div>
    );
  }
}

export default OnlyArts;
