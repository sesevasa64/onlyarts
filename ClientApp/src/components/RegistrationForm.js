import React from 'react';

import './css/Input-box.css' ;
import '../OnlyArts.css';
import MD5 from '../models/md5.js';
import HashModel from '../models/HashModel.js';
let hash_model = new HashModel(MD5);


class RegistrationForm extends React.Component
{
    constructor(props)
    {
        super(props);
        this.onRegButtonClick = this.onRegButtonClick.bind(this);
    }
    state = {
        login: "",
        email: "",

        password: "",
        passwordConfirm: "",
        errorPassword: "Введите пароль",
        re_pass: /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/,

        email_placeholder: "aboba@gmail.com",
        re_email: /^[\w]{1}[\w-\.]*@[\w-]+\.[a-z]{2,4}$/i,
        email_class: "",
    };

    onLoginChange = event => this.setState({
        login: event.target.value,
    });

    onEmailChange = event => this.setState({
        email: event.target.value,
    });

    onPasswordChange = event => {
        if(event.target.id === "password")
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
        let password_hash = "";
        if(passwordConfirm !== password)
        {
            error = "Пароли не равны!";
        }
        else
        {
            if(this.state.re_pass.test(password))
            {
                password_hash = hash_model.get_hash(password);
                console.log(password_hash);
                error = "";
            }
            else
            {
                error = "Проверьте пароль длиной от 6 до 20 символов, который содержит хотя бы одну цифровую цифру , " +
                        + "одну заглавную и одну строчную букву";
            }
        }
        return this.setState({
            errorPassword: error,
        });
    }

    onRegButtonClick(event)
    {
        let email_placeholder = "aboba@gmail.com";
        let email_class = ""; 
        if(!this.state.re_email.test(this.state.email))
        {
            email_placeholder = "Емейл введен некорректно!";
            email_class = "unorrect-input";
        }
        else
        {
            email_class = "correct-input";
        }
        this.setState({
            email_placeholder: email_placeholder,
            email_class: email_class,
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
                            <label>Введите логин</label>
                            <input type="text" placeholder="anton-aboba-228" onChange={this.onLoginChange} />
                            <label className={!this.state.email_class || "uncorrect-output"}>{this.state.email_placeholder}</label>
                            <input className={this.state.email_class} type="text" placeholder="aboba@gmail.com" onChange={this.onEmailChange}/>
                            <label className={!this.state.errorPassword || "uncorrect-output"}>{this.state.errorPassword}</label>
                            <input id="password" type="password" onChange={this.onPasswordChange}/>
                            <label>Повторите пароль</label>
                            <input id="passwordConfirm" type="password" onChange={this.onPasswordChange} onBlur={this.onPasswordConfirmBlur}/>
                            <button onClick={this.onRegButtonClick}>Зарегистрироваться</button>
                        </div>
                    </div>
                    <p onClick={()=> this.props.close_onClick(false)}>Закрыть</p>
                </div>
            </div>
        );
    }
}

export default RegistrationForm;

/*
<label>Код подтверждения регистрации отправлен вам на почту {this.state.email}</label>
                            <label>Введите код подтверждения</label>
                            <input type="text"/>
                            <button>Переотправить</button>
*/