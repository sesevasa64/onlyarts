import React from 'react';
import './Button.css'
import './HeaderOA.css'

function HeaderOA()
{
    return (
    <div className="box-header">
      <div id='div-header-search-box'>
        <div id="div-header-search-element">
          <input className="SearchInput" type="text"></input>
          <input type="button" value="Найти"></input>
        </div>
      </div>
    </div>
    );
}

export default HeaderOA;
