import React from 'react';
import RoundButton from './RoundButton';
import {Link} from 'react-router-dom';

import './css/Button.css'
import './css/HeaderOA.css'
import gear from "./resources/gear.svg"

function HeaderOA(props)
{
    return (
    <div className="box-header">
      <div id='div-header-search-box'>
        <div id="div-header-search-element">
        <div className="gcse-search"></div>
        </div>
      </div>
      <div className="box-header-left">
        {props.isAuth || <RoundButton onClick={props.onLoginClick} value={"Логин"}/>}
        {!props.isAuth || <img width="32px" height="32px" src={props.User.LinkToAvatar}/>}
        {!props.isAuth || <p>{props.User.Login}</p>}
        {!props.isAuth || <Link to="/EditUser/"><img width="32px" height="32px" src={gear}/></Link>}
        {!props.isAuth || <RoundButton onClick={props.onExitClick} value={"Выйти"}></RoundButton>}
      </div>
    </div>
    );
}

export default HeaderOA;
