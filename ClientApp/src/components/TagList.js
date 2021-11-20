import React from 'react';
import './css/TagList.css'

function TagList(props)
{
    const tags_list = [];
    for(let i = 0; i < 9; i++)
        tags_list.push(
            <li key={i}>Tags{i}</li>
        );
    return(
        <div className="box-tags">
                <p>Популярные теги</p>
                <ul className="tag-list">
                    {tags_list}
                </ul>
        </div>
    );
}

export default TagList;
