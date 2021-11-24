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


class OnlyArts extends Component
{
  constructor(props)
  {
    super(props);
    this.state = 
    {
      content_cards: [],
      current_content: 0,

      tags: [],

      outputLoginBox: false,
      outputRegistationForm: false,
      contentIsSelect: false,
      selectedContent: null,
	    
      isAuth: false,
      user:{
        Login: ""
      },
    }
  }

  componentWillMount()
  {
    this.userExit = this.userExit.bind(this);
    this.checkAuth();
    this.loadPopularCards(0, 3);
    this.loadPopularTags(0, 3);
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
    let response = await fetch(`https://localhost:5001/api/tags/popular?min=${from}&max=${to}`)
    if(response.ok)
    {
      tags = await response.json();
      this.setState({
        tags: tags,
      })
    }
  }

  async loadPopularCards(from, to)
  {
    let card;
    let answer = []; // Карточки на стринице
    let response = await fetch(`https://localhost:5001/api/contents/popular?min=${from}&max=${to}`)
    if(response.ok)
    {
      card = await response.json();
      answer = card;
      answer = [...answer, ...answer, ...answer, ...answer, ...answer, ...answer];
    }
    this.setState({
       content_cards: answer,
    });
  }

  async getContentById(contentId)
  {
    let response = await fetch(`https://localhost:5001/api/contents/${contentId}`);
    let json = null;
    if(response.ok)
    {
      let json = await response.json();
    }
    return json;
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
    this.loadPopularCards(0, 3);
  }
  
  checkAuth()
  {
    let authToken = getCookie("authToken");
    let login = getCookie("Login");
    if(authToken && login)
    {
      console.log(login);
      this.setState({
        authToken: authToken,
        user: {
          Login: login
        },
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
      user: {
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
      user:{
        login: ""
      }
    })
    deleteCookie("authToken");
    deleteCookie("Login");
  }

  authFunc(user, rememberAuth)
    {
        /*
        User is var with fields: {
            Login: "",
            Password: ""
        }
        */
        fetch(`https://localhost:5001/api/users/auth`,{
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(user)
        })
        .then((response) => {
            if(response.ok){
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
              this.userAuthorize(result.authToken, user.Login, rememberAuth)
            }
        });
    }

  async onLikeClick(content_id)
  {
    let response = await fetch(`https://localhost:5001/api/content/${content_id}/like`,{
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
    console.log(this.state.isAuth);
    return (
      <div className="main-box">
          {(this.state.outputLoginBox && !this.state.isAuth) ? <AuthForm authFunc={this.authFunc} closeBox={this.renderLoginBox} reg_onClick = {this.renderRegistrationForm}/> : "" }
          {!this.state.outputRegistationForm || <RegistrationForm authFunc={this.authFunc} close_onClick={this.renderRegistrationForm}/>}
          <div className="top-flex-div">
            <Logo/>
            <HeaderOA isAuth={this.state.isAuth} user={this.state.user} onLoginClick={this.renderLoginBox} onExitClick={this.userExit}/>
          </div>
          <div className="menu-flex-div">
            <NavigationMenu user={this.state.user} isAuth={this.state.isAuth}/>
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
                                              content={this.state.content_cards}
                                              title={"Главная страница"}/>}/>
              <Route path="/NewPost/" render={() => <NewPostPage/>}/>
            </Switch>
          </div>
      </div>
    );
  }
}

export default OnlyArts;
