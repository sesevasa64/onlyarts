import React from 'react';
import { Redirect } from 'react-router';

import './css/Input-box.css' ;
import '../OnlyArts.css';
import MD5 from '../models/md5.js';
import HashModel from '../models/HashModel.js';
let hash_model = new HashModel(MD5);
let host_name = "https://" + document.location.host;

class RegistrationForm extends React.Component
{
    constructor(props)
    {
        super(props);
        this.onRegButtonClick = this.onRegButtonClick.bind(this);
    }
    state = {
        login: "", // Строка, содержащая логин
        login_error_str: "", // Строка, которая выводится, если логин несоответствует норме
        login_correct: true, // Значение, отвечающее за корректность ввода

        nickname: "", 
        nickname_error_str: "",
        nickname_correct: true,

        password: "", //Поле, содержащее пароль
        passwordConfirm: "", //Поле, содержащее повторный ввод пароля
        password_error_str: "Введите пароль",
        re_pass: /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/, //Регулярное выражение, проверяющее корректность ввода пароля
        password_correct: true, // Значение, отвечающее за корректность ввода

        email: "",
        email_placeholder: "Введите е-мейл",
        re_email: /^[\w]{1}[\w-\.]*@[\w-]+\.[a-z]{2,4}$/i,
        email_class: "",
        email_correct: true, // Значение, отвечающее за корректность ввода

        linkToAvatar: "https://wallsdesk.com/wp-content/uploads/2016/04/Madison-Ivy-High-Quality-Wallpapers.jpg", //Ссылка на аватар

        regIsCorrect: false,
    };

    onLoginChange = event => this.setState({
        login: event.target.value,
    });

    onEmailChange = event => this.setState({
        email: event.target.value,
    });
    onLinkAvatarChange = (event)=>
    {   
        if(event.target.value){
            this.setState({
                linkToAvatar: event.target.value,
            });
        }
        else{
            this.setState({
                linkToAvatar: "https://wallsdesk.com/wp-content/uploads/2016/04/Madison-Ivy-High-Quality-Wallpapers.jpg",
            });
        }
    }

    onNicknameChange = (event) =>this.setState({nickname: event.target.value,})
    
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
                error = "";
                this.setState({
                    password_correct: true,
                    password_hash: password_hash,
                })
            }
            else
            {
                error = "Пароль должен быть длиной от 6 до 20 символов, который содержит хотя бы одну цифровую цифру, одну заглавную и одну строчную букву";
            }
        }
        return this.setState({
            errorPassword: error,
        });
    }

    /*
        Метод, обрабатывающий нажатие на кнопку "Регистрация"
    */

    onRegButtonClick(event)
    {
        let email_placeholder = "aboba@gmail.com"; //Плейсхолдер, вставляющийся в поле для воода email
        let email_class = "";  //класс стиля для лейбла, расположенного над полев ввода email
        let email_correct = false; //флаг, отвечающий за понимание того, корректно ли был введен email
        //Проверяем корректность email
        if(!this.state.re_email.test(this.state.email))
        {
            email_placeholder = "Емейл введен некорректно!";
            email_class = "unorrect-input";
        }
        else
        {
            email_class = "correct-input";
            email_correct = true;
        }
        let login_correct = false; //Плейсхолдер, вставляющийся в поле для воода login
        let login_class = ""; //Класс стиля для лейбла, расположенного над полев ввода login
        let login_error_str = ""; //Флаг, отвечающий за понимание того, корректно ли был введен login
        //Проверяем корректность логина
        if(this.state.login.length > 5 && this.state.login.length < 21)
        {
            login_correct = true;
            login_class = "correct-input";
        }
        else
        {
            login_class = "uncorrect-input";
            login_error_str = "Пароль должен быть от 6 до 20 символов";
        }
        //Проверяем корректность никнейма
        let nickname_class = ""; //Плейсхолдер, вставляющийся в поле для воода nickname
        let nickname_correct = false; //класс стиля для лейбла, расположенного над полев ввода nickname
        let nickname_error_str = ""; //флаг, отвечающий за понимание того, корректно ли был введен nickname

        if(this.state.nickname.length > 5 && this.state.nickname.length < 21)
        {
            nickname_correct = true;
            nickname_class = "correct-input";
        }
        else
        {
            nickname_class = "uncorrect-input";
            nickname_error_str = "Никнейм должен быть от 6 до 20 символов";
        }


        this.setState({
            email_placeholder: email_placeholder,
            email_class: email_class,
            email_correct: email_correct,

            login_error_str: login_error_str,
            login_correct: login_correct,
            login_class: login_class,

            nickname_error_str: nickname_error_str,
            nickname_correct: nickname_correct,
            nickname_class: nickname_class,
        });

        if(email_correct&&this.state.password_correct
            &&login_correct && nickname_correct)
        {
            this.postRegistration();
        }
    }

    postRegistration()
    {
        let user = {
            Login: this.state.login,
            Password: this.state.password_hash,
            Email: this.state.email,
            Nickname: this.state.nickname,
            LinkToAvatar: this.state.linkToAvatar ? this.state.linkToAvatar : null
        }
        fetch(`${host_name}/api/users`,{
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(user)
        })
        .then((response) => {
            if(response.ok){
                this.setState({
                    regIsCorrect: true, //Устанавливаем, что регистрация прошла успешно
                });
                this.props.authFunc({
                    Login: user.Login,
                    Password: user.Password
                }, true)
                return 1;
            }
            else{
                return 0;
            }
        })
    }

    render()
    {
        //Если регистрация прошла успешно 
        if(this.state.regIsCorrect)
        {
            return (
                <div className="absolute-form">
                    <div className="auth">
                        <div className="center-input-box">
                            <p>Регистрация прошла успешно</p>
                            <div className="center-input-box-input-container">
                                <button onClick={()=> this.props.close_onClick(false)}>Закрыть</button>
                            </div>
                        </div>
                    </div>
                </div>
            )
        }
        return(
            <div className="absolute-form">
                <div className="auth">
                    <div className="center-input-box">
                        <p>Регистрация {this.state.login}</p>
                        <div className="center-input-box-input-container">
                            <label>{!this.state.login_error_str ? "Введите логин *" : this.state.login_error_str}</label>
                            <input className={this.state.login_class} type="text" placeholder="anton-aboba-228" onChange={this.onLoginChange} />
                            <label>{!this.state.nickname_error_str ? "Введите никнейм *" : this.state.nickname_error_str}</label>
                            <input className={this.state.nickname_class} type="text" placeholder="anton-aboba-228" onChange={this.onNicknameChange} />
                            <label className={!this.state.email_class || "uncorrect-output"}>{this.state.email_placeholder + '*'}</label>
                            <input className={this.state.email_class} type="text" placeholder="aboba@gmail.com" onChange={this.onEmailChange}/>
                            <label className={this.state.errorPassword ? "uncorrect-output" : "correct-output"}>
                                {this.state.errorPassword ? this.state.errorPassword : "Введите пароль"}
                            </label>
                            <input id="password" type="password" onChange={this.onPasswordChange}/>
                            <label>Повторите пароль</label>
                            <input id="passwordConfirm" type="password" onChange={this.onPasswordChange} onBlur={this.onPasswordConfirmBlur}/>
                            <label>Укажите информацию о себе</label>
                            <textarea></textarea>
                            <label>Укажите ссылку на аватар</label>
                            <input id="linkToAvatar" type="text" onChange={this.onLinkAvatarChange}/>
                            <div>
                                <img width="100px" height="100px" src={this.state.linkToAvatar}></img>
                            </div>
                            <button onClick={this.onRegButtonClick}>Зарегистрироваться</button>
                            <button onClick={()=> this.props.close_onClick(false)}>Закрыть</button>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default RegistrationForm;
