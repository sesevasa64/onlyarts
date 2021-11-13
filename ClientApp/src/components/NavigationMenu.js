import React from 'react';
import './NavigationMenu.css'

function NavigationMenu()
{
    const nav_list = [];
    for(let i = 0; i < 5; i++)
        nav_list.push(
            <li key={i}><a>Menu{i}</a></li>
        );
    return(
        <div className="box-nav">
            <ul className="menu-main">
                <li><a>Создать пост</a></li>
                <li><a>Моя  страница</a></li>
                <li><a>Мои подписки</a></li>
            </ul>
        </div>
    );
}

export default NavigationMenu;
