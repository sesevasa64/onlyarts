import React from 'react';
import RoundButton from './RoundButton';

import './Button.css'
import './HeaderOA.css'

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
      <div>
        <RoundButton onClick={props.onLoginClick} value={"Логин"}/>
      </div>
    </div>
    );
}

export default HeaderOA;
