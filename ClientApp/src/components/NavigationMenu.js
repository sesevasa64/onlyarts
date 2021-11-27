import React from 'react';
import {Link} from 'react-router-dom'
import './css/NavigationMenu.css'

function NavigationMenu(props)
{
    const nav_list = [];
    for(let i = 0; i < 5; i++)
        nav_list.push(
            <li key={i}><a>Menu{i}</a></li>
        );
    return(
        <div className="box-nav">
            <ul className="menu-main">
                <li><Link to="/">Главная страница</Link></li>
                <li><a>Популярные</a></li>
                {!props.isAuth || <li><Link to="/NewPost/">Создать пост</Link></li>}
                {!props.isAuth || <li><Link to={`/UserPage/${props.User.Login}`}>Моя страница</Link></li>}
                {!props.isAuth || <li><a>Мои подписки</a></li>}
            </ul>
        </div>
    );
}

export default NavigationMenu;
