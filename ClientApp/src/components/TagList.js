import React from 'react';
import './css/TagList.css'

function TagList(props)
{
    const tags_list = [];
    for(let i = 0; i < props.tags.length; i++)
        tags_list.push(
            <li key={i} onClick={() => props.selectTag(props.tags[i].TagName)}>{props.tags[i].TagName}</li>
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
