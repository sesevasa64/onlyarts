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
        login:""
      },
    }
    this.loadPopularCards();
    this.userExit = this.userExit.bind(this);
	  this.userAuthorize = this.userAuthorize.bind(this);
    this.getContentById = this.getContentById.bind(this);
    this.renderLoginBox = this.renderLoginBox.bind(this);
    this.renderRegistrationForm = this.renderRegistrationForm.bind(this);
    this.renderSelectedContent = this.renderSelectedContent.bind(this);
  }

  async loadPopularCards()
  {
    let response = await fetch("https://localhost:5001/api/contents/1")
    let response2 = await fetch("https://localhost:5001/api/contents/2")
    console.log("asdas");
    if(response.ok)
    {
      let json = await response.json();
      let json1 = await response2.json();
      json = [json[0], json1[0]];
      this.setState({
        content_cards: json,
      });
    }
    else
    {
      alert("Ошибка HTTP: " + response.status);
    }
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
       login: login
     },
	 })
  }
  userExit(login)
  {
    this.setState({
      isAuth: false,
      user:{
        login: ""
      }
    })
  }

  render () {
    return (
      <div className="main-box">
          {(this.state.outputLoginBox && !this.state.isAuth) ? <AuthForm authFunc={this.userAuthorize} closeBox={this.renderLoginBox} reg_onClick = {this.renderRegistrationForm}/> : "" }
          {!this.state.outputRegistationForm || <RegistrationForm close_onClick={this.renderRegistrationForm}/>}
          <Logo/>
          <HeaderOA isAuth={this.state.isAuth} user={this.state.user} onLoginClick={this.renderLoginBox} onExitClick={this.userExit}/>
          <NavigationMenu isAuth={this.state.isAuth}/>
          <TagList/>
          <Switch>
            <Route path={'/ContentPage/:contentId'}> 
              <ContentPage/>
            </Route>
            <Route path={'/UserPage/:login'}>
              <UserPage content={<CardsContentBox content_onClick={this.renderSelectedContent}
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
