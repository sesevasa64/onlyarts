import React, { Component } from 'react';
import '../OnlyArts.css'


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
            Password: this.state.password
        }
        console.log("dasdas");
        console.log(JSON.stringify(user));
        fetch(`https://localhost:5001/api/users/auth`,{
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(user)
        })
        .then((response) => {
            if(response.ok)
            {
                return response.json()
            }
            else
            {
                return 0;
            }
        })
        .then((result) => 
        {
            if(result)
            {
                this.props.authFunc(result.authToken, user.Login)
            }
        });
    }
    loginChange = (event) => this.setState({login: event.target.value,})
    passwordChange = (event) => this.setState({password: event.target.value,})
    
    render()
    {
        return (
            <div className="absolute-form">
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
                        <input type="button" value="Регистрация" onClick={function (){
                        this.props.closeBox(false);
                        this.props.reg_onClick(true);
                        return;
                        }}/>
                    </div>
                    <button onClick={()=>this.props.closeBox(false)}>
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
