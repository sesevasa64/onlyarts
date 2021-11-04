import React from 'react';

function NavigationMenu()
{
    const nav_list = [];
    for(let i = 0; i < 9; i++)
        nav_list.push(
            <li key={i}>Menu{i}</li>
        );
    return(
        <div className="box-nav">
            <ul className="top-menu">
                {nav_list}
            </ul>
        </div>
    );
}

export default NavigationMenu;
