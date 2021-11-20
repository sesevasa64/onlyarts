import React, { Component } from 'react';
import {Route, NavLink, HashRouter,
        BrowserRouter,
        Switch,
        Link
} from 'react-router-dom'

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


class OnlyArts extends Component
{
  constructor(props)
  {
    super(props);
    this.state = 
    {
      content_cards: [],
      current_content: 0,
      outputLoginBox: false,
      outputRegistationForm: false,
      contentIsSelect: false,
      selectedContent: null,
	    
      isAuth: false,
      user:{
        Login:""
      },
    }
    this.loadPopularCards();
    this.onLikeClick = this.onLikeClick.bind(this);
    this.authFunc = this.authFunc.bind(this);
    this.userExit = this.userExit.bind(this);
	  this.userAuthorize = this.userAuthorize.bind(this);
    this.getContentById = this.getContentById.bind(this);
    this.renderLoginBox = this.renderLoginBox.bind(this);
    this.renderRegistrationForm = this.renderRegistrationForm.bind(this);
    this.renderSelectedContent = this.renderSelectedContent.bind(this);
  }

  async loadPopularCards()
  {
    let card;
    let answer = []; // Карточки на стринице
    let response = await fetch("https://localhost:5001/api/contents/1")
    let i = 2;
    while(response.ok)
    {
      card = await response.json();
      answer.push(card);
      response = await fetch(`https://localhost:5001/api/contents/${i}`)
      i++;
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
    this.loadPopularCards();
  }
  
  userAuthorize(authToken, login)
  {
	 this.setState({
		 authToken: authToken,
		 isAuth: true,
		 outputLoginBox: false,
     user: {
       Login: login
     },
	 })
  }
  userExit(login)
  {
    this.setState({
      isAuth: false,
      user:{
        Login: ""
      }
    })
  }

  authFunc(user)
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
              this.userAuthorize(result.authToken, user.Login)
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
    return (
      <div className="main-box">
          {(this.state.outputLoginBox && !this.state.isAuth) ? <AuthForm authFunc={this.authFunc} closeBox={this.renderLoginBox} reg_onClick = {this.renderRegistrationForm}/> : "" }
          {!this.state.outputRegistationForm || <RegistrationForm authFunc={this.authFunc} close_onClick={this.renderRegistrationForm}/>}
          <Logo/>
          <HeaderOA isAuth={this.state.isAuth} user={this.state.user} onLoginClick={this.renderLoginBox} onExitClick={this.userExit}/>
          <NavigationMenu user={this.state.user} isAuth={this.state.isAuth}/>
          <TagList/>
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
            <Route path="/" render={()=><CardsContentBox content_onClick={this.renderSelectedContent}
                                            content={this.state.content_cards}
                                            title={"Главная страница"}/>}/>
          </Switch>
      </div>
    );
  }
}

export default OnlyArts;
