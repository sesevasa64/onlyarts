import React, { Component } from 'react';
import '../OnlyArts.css'

import MD5 from '../models/md5.js';
import HashModel from '../models/HashModel.js';
let hash_model = new HashModel(MD5);

class AuthForm extends Component
{
    constructor(props)
    {
        super(props);
        this.state = {
            login: "",
            password: "",  
        }
        this.auth_onClick = this.auth_onClick.bind(this);
    }

    auth_onClick()
    {
        let user = {
            Login: this.state.login,
            Password: hash_model.get_hash(this.state.password)
        }
        this.props.authFunc(user);
    }
    loginChange = (event) => this.setState({login: event.target.value,})

    passwordChange = (event) => this.setState({password: event.target.value,})
    
    onAuthRender = () => 
    {
        this.state.setState({
            absolute_form: document.getElementsByClassName('absolute-form')[0],
        })
    }

    render()
    {
        return (
            <div className="absolute-form" onClick={(event)=> event.target == this.state.absolute_form ? event : null}>
                <div className="auth">
                    <h2>Добро пожаловать</h2>
                    <div id="auth-top">
                        <h2>Вход</h2>
                        <LoginForm loginChange={this.loginChange} passwordChange={this.passwordChange} 
                        auth_onClick={this.auth_onClick}/>
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
                        <input type="button" value="Регистрация" onClick={() => {
                            this.props.closeBox(false);
                            this.props.reg_onClick(true);
                        return;
                        }}/>
                    </div>
                    <button onClick={(event)=> this.props.closeBox(false)}>
                        Закрыть
                    </button>
                </div>
            </div>
        )
    }
}

function ResourcesLine()
{
  return(
  <div className="resources-container">
                    <div className="another-resource divs-in-line">
                        <img src="./resources/vk_icon_32x32.png" />
                    </div>
                    <div className="another-resource divs-in-line">
                        <img src="./resources/google_icon_32x32.png"/>
                    </div>
                    <div className="another-resource divs-in-line">
                        <img src="./resources/facebook_icon_32x32.png" />
                    </div>
    </div>
  )
}

function LoginForm(props)
{
  return (
    <form>
      <div id="data-enter">
                    <div className="input-container">
                        <p className="default-text">Телефон или электронная почта</p>
                        <input onChange={props.loginChange} className="StandartInput" id="input-login" type="text"/>
                    </div>
                    <div className="input-container">
                        <p className="default-text">Пароль</p>
                        <input onChange={props.passwordChange} className="StandartInput" id="input-password" type="password"/>
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
                <input onClick={props.auth_onClick} type="button" value="Войти"/>
    </form>
  )
}



export default AuthForm;
