import React from 'react';

import './Input-box.css' 
import '../OnlyArts.css'
class RegistrationForm extends React.Component
{
    constructor(props)
    {
        super(props);
    }
    state = {
        login: "",
        email: "",
        password: "",
        passwordConfirm: "",
        errorPassword: "",
    };

    onLoginChange = event => this.setState({
        login: event.target.value,
    });

    onEmailChange = event => this.setState({
        email: event.target.value,
    });

    onPasswordChange = event => {
        if(event.target.id == "password")
            return this.setState({
                password: event.target.value,
            });
        else
            return this.setState({
                passwordConfirm: event.target.value,
            });
    }

    onPasswordConfirmBlur = () =>
    {
        let error;
        const passwordConfirm = this.state.passwordConfirm;
        const password = this.state.password;
        if(passwordConfirm != password)
            error = "Passwords is not equals!";
        else
            error = "";
        return this.setState({
            errorPassword: error,
        });
    } 

    render()
    {
        return(
            <div className="absolute-form">
                <div className="auth">
                    <div className="center-input-box">
                        <p>Регистрация {this.state.login}</p>
                        <div className="center-input-box-input-container">
                            <form>
                            <label>Введите логин</label>
                            <input type="text" placeholder="anton-aboba-228" onChange={this.onLoginChange} />
                            <label>Введите почту</label>
                            <input type="text" placeholder="aboba@gmail.com" onChange={this.onEmailChange}/>
                            <label>Введите пароль</label>
                            <input id="password" type="password" onChange={this.onPasswordChange}/>
                            <label>Повторите пароль</label>
                            <input id="passwordConfirm" type="password" onChange={this.onPasswordChange} onBlur={this.onPasswordConfirmBlur}/>
                            <label>{this.state.errorPassword}</label>
                            <button>Зарегистрироваться</button>
                            </form>
                            <label>Код подтверждения регистрации отправлен вам на почту {this.state.email}</label>
                            <label>Введите код подтверждения</label>
                            <input type="text"/>
                            <button>Переотправить</button>
                        </div>
                    </div>
                    <p onClick={()=> this.props.close_onClick(false)}>Закрыть</p>
                </div>
            </div>
        );
    }
}

export default RegistrationForm;
