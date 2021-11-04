import React from 'react';

function TagList(props)
{
    const tags_list = [];
    for(let i = 0; i < 9; i++)
        tags_list.push(
            <li key={i}>Tags{i}</li>
        );
    return(
        <div className="box-tags">
            <ul>
                {tags_list}
            </ul>
        </div>
    );
}

export default TagList;
