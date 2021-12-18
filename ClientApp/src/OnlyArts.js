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
import { SubscribersPage } from './components/SubscribersPage';

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
      title: "Главная страницы"
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

    if(response.ok)
    {
      card = await response.json();
      answer = card;
    }
    if(callback)
    {
      callback(response.ok);
    }
    this.setState({
       content_cards: answer,
       title: "Главная страницы"
    });
  }

  async loadPopularCardsByTagName(from, to, callback, tagname)
  {
    let card;
    let answer = []; // Карточки на стринице
    let response = await fetch(`${host_name}/api/contents/popular/${tagname}?min=${from}&max=${to}`);
    if(response.ok)
    {
      card = await response.json();
      answer = card;
    }
    if(callback)
    {
      callback(response.ok);
    }
    this.setState({
       content_cards: answer,
       title: tagname
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
  /*
  Метод возвращающий контент пользователя по логину
  login - логин пользователя
  min - самый популярный
  max - популярный контент на позииции max
  callback(contents, sucLoad) - функция, передастся массив значений карточек и значение типа bool, указывающее на успешность запроса, где true - успешен, false - не успешен.  
  */
  async getContentByLogin(login, min, max, callback)
  {
    fetch(`${host_name}/api/contents/popular/user/${login}?min=${min}&max=${max}`)
    .then((response) => {
      if(response.ok)
      {
        let contents  = response.json();
        return contents;
      }
      else
      {
        callback([], false);
      }
    })
    .then((value)=>
    {
      if(value)
      {
        callback(value, true);
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
      "Name": Name1,
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
    fetch(`${host_name}/api/contents`, json_to_post).
    then((response) =>{
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

  async onLikeClick(content_id, userID)
  {
    
    let response = await fetch(`${host_name}/api/contents/${content_id}/likes?userID=${userID}`,{
      method: 'PATCH',
      headers: {
        'Content-Type': 'application/json',
        'API-key': this.state.authToken
      }
    });
    
  }

  checkLikeUser(content_id, userID, callback)
  {
    fetch(`${host_name}/api/reactions/like?userId=${userID}&contentId=${content_id}`)
    .then((response)=>{
      callback(response.ok)
    })
  }
  /* 
   Функция отправляющая PUT запрос на изменение информации о пользователе
   Аргументы:
   User is var with fields: {
            Id: "",
            Nickname: "",
            Info: ""
        }
  */
  changeUserInfo(UserInfo, callback)
  {
    if(UserInfo)
    {
      fetch(`${host_name}/api/users`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(UserInfo)
      })
      .then((response)=>{
        callback(response.ok)
      })
    }  
    else
    {
      callback(false)
    }

  }

  getUserSubscribers(user_login, min, max, callback)
  {
    fetch(`${host_name}/api/users/subscribers/${user_login}?min=${min}&max=${max}`)
    .then((response) => response.json())
    .then((value) => {
      if(value){
        callback(value);
      }
    })
  }

  subscribeOnUser(user_id, author_id, sub_id, callback)
  {
    let SubscribeObject = {
      AuthorId: author_id,
      SubUserId: user_id,
      SubTypeId: sub_id
    }

    fetch(`${host_name}/api/users/subscribe`, {
      method: "POST",
      headers: {
        'Content-Type': 'application/json;charset=utf-8'
      },
      body: JSON.stringify(SubscribeObject)
    })
    .then((response) => {
      callback(response.ok)
    })
  }

  unsubscribeOnUser(user_id, author_id)
  {
    fetch(`${host_name}/api/users/unsubscribe?authorID=${author_id}&subuserID=${user_id}`, {
      method: "POST",
      headers: {
        'Content-Type': 'application/json;charset=utf-8'
      }
    });
  }


  checkSubscriberUser(author_id, user_id, callback)
  {
    fetch(`${host_name}/api/subs/check?authorId=${author_id}&userId=${user_id}`)
    .then((response) => callback(response.ok));
    console.log(`${author_id} ${user_id}`)
  }


  patchViewToContent(contentId, callback)
  {
    fetch(`${host_name}/api/contents/${contentId}/view`, {
      method: "PATCH",
      headers: {
        "Content-Type": "application/json"
      }
    })
    .then((response)=>{
      if(callback)
      {
        callback(response.ok);
      }
    })
  }


  render () {
    console.clear();
    return (
      <div className="main-box">
          {(this.state.outputLoginBox && !this.state.isAuth) ? <AuthForm authFunc={this.authFunc} closeBox={this.renderLoginBox} reg_onClick = {this.renderRegistrationForm}/> : "" }
          {!this.state.outputRegistationForm || <RegistrationForm authFunc={this.authFunc} close_onClick={this.renderRegistrationForm}/>}
          <div className="top-flex-div">
            <Logo/>
            <HeaderOA isAuth={this.state.isAuth} User={this.state.User}
            onLoginClick={this.renderLoginBox} 
            onExitClick={this.userExit}/>
          </div>
          <div className="menu-flex-div">
            <NavigationMenu onPopularClick={this.loadPopularCards} User={this.state.User} isAuth={this.state.isAuth}/>
          </div>
          <div className="center-flex-div">
            <TagList selectTag={(tagname)=> this.loadPopularCardsByTagName(0, 18, ()=>{}, tagname)} tags={this.state.tags}/>
            <Switch>
              <Route path={'/ContentPage/:contentId'}> 
                <ContentPage User={this.state.User} 
                             addViewToContent={this.patchViewToContent}
                             onLikeClick={this.onLikeClick}
                             checkLike={this.checkLikeUser}/>
              </Route>
              <Route path={'/UserPage/:login'}>
                <UserPage
                User = {this.state.User} 
                checkSubscriberUser = {this.checkSubscriberUser}
                loadUserContent={this.getContentByLogin}
                subscribeOnUser = {this.subscribeOnUser}
                renderSelectedContent = {this.renderSelectedContent}
                getSubscribers={this.getUserSubscribers}/>
              </Route>
              <Route exact path="/" render={()=><CardsContentBox content_onClick={this.renderSelectedContent}
                                              loadContent={this.state.loadContent} content={this.state.content_cards} title={this.state.title}/>}/>
              <Route path="/NewPost/" render={() => <NewPostPage User={this.state.User} addNewPost={this.addNewPost}/>}/>
              <Route path="/EditUser/" render={() => 
                <EditUserPage User={this.state.User} changeUserInfo={this.changeUserInfo}>
                </EditUserPage>}>
              </Route>
            </Switch>
          </div>
      </div>
    );
  }
}

export default OnlyArts;
