import React, { Component } from 'react';
import {Route, NavLink, HashRouter} from 'react-router-dom'

import './OnlyArts.css'

import HeaderOA from './components/HeaderOA';
import Logo from './components/Logo';
import NavigationMenu from './components/NavigationMenu';
import TagList from './components/TagList';
import CardsContentBox from './components/CardsContentBox';
import ContentPage from './components/ContentPage';
import RegistrationForm from './components/RegistrationForm';
import {t1, t2, t3, t4} from './models/TestContentCard'

class OnlyArts extends Component
{
  constructor(props)
  {
    super(props);
    this.state = 
    {
      content_cards: [t1, t2, t3, t4],
      current_content: 0,
      outputLoginBox: false,
      outputRegistationForm: false, 
    }
    this.renderLoginBox = this.renderLoginBox.bind(this);
    this.renderRegistrationForm = this.renderRegistrationForm.bind(this);
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
  }

  render () {
    
    var data = () => <AuthBox/>;
    
    return (
      <div className="main-box">
          {!this.state.outputLoginBox || <AuthBox closeBox={this.renderLoginBox} reg_onClick = {this.renderRegistrationForm}/>}
          {!this.state.outputRegistationForm || <RegistrationForm close_onClick={this.renderRegistrationForm}/>}
          <Logo/>
          <HeaderOA onLoginClick={this.renderLoginBox}/>
          <NavigationMenu/>
          <TagList/>
          <Route path="/ContentPage/" render={() => <ContentPage item={this.state.content_cards[this.state.current_content]}/>}/>
          <Route path="/" render={() => <CardsContentBox content={this.state.content_cards}/>}/>
      </div>
    );
  }
}

/*
"https://i.ytimg.com/vi/4lifQfeZo5c/maxresdefault.jpg", 
                       "https://cdn-ru0.puzzlegarage.com/img/puzzle/5/5765_preview_r.v1.jpg",
                       "https://avatars.mds.yandex.net/get-zen_doc/1880741/pub_60ebc44a0f1e1b2a8ceb58e7_60ebc559b56ded7a70c250ed/scale_1200"],
*/

function AuthBox(props)
{
  return (
    <div className="absolute-form">
      <div className="auth">
            <h2>Добро пожаловать</h2>
            <div id="auth-top">
                <h2>Вход</h2>
                <LoginForm/>
            </div>
            <div id="data-enter-help">
                <div>
                    <hr/>
                    <p>Войти при помощи</p>
                    <hr/>
                </div>
                <ResourcesLine/>
                <hr/>
            </div>
            <div id="auth-bottom">
                <input type="button" value="Регистрация" onClick={function (){
                  props.closeBox(false);
                  props.reg_onClick(true);
                  return;
                }}/>
            </div>
            <button onClick={()=>props.closeBox(false)}>
              Закрыть
            </button>
        </div>
    </div>
  )
}

function ResourcesLine()
{
  return(
  <div class="resources-container">
                    <div class="another-resource divs-in-line">
                        <img src="./resources/vk_icon_32x32.png" />
                    </div>
                    <div class="another-resource divs-in-line">
                        <img src="./resources/google_icon_32x32.png"/>
                    </div>
                    <div class="another-resource divs-in-line">
                        <img src="./resources/facebook_icon_32x32.png" />
                    </div>
    </div>
  )
}

function LoginForm()
{
  return (
    <form>
      <div id="data-enter">
                    <div className="input-container">
                        <p className="default-text">Телефон или электронная почта</p>
                        <input className="StandartInput" id="input-login" type="text"/>
                    </div>
                    <div className="input-container">
                        <p className="default-text">Пароль</p>
                        <input className="StandartInput" id="input-password" type="password"/>
                    </div>
                </div>
                <div id="data-help" className="input-container">
                    <div className="divs-in-line">
                        <input type="checkbox"/> Запомнить
                    </div>
                    <div className="divs-in-line">
                        <a href="./test.html">Забыли пароль?</a>
                    </div>
                </div>
                <input type="button" value="Войти"/>
    </form>
  )
}



export default OnlyArts;
