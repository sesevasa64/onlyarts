import React from 'react';
import RoundButton from './RoundButton';

import './css/Button.css'
import './css/HeaderOA.css'

function HeaderOA(props)
{
    return (
    <div className="box-header">
      <div id='div-header-search-box'>
        <div id="div-header-search-element">
          <input className="SearchInput" type="text"></input>
          <input type="button" value="Найти"></input>
        </div>
      </div>
      <div className="box-header-left">
        {props.isAuth || <RoundButton onClick={props.onLoginClick} value={"Логин"}/>}
        {!props.isAuth || <p>{props.user.Login}</p>}
        {!props.isAuth || <RoundButton onClick={props.onExitClick} value={"Выйти"}></RoundButton>}
      </div>
    </div>
    );
}

export default HeaderOA;
