import React from 'react'
import './RoundButton.css'

function RoundButton(props)
{
    return(
        <button className="round-button" onClick={() => props.onClick(true)}>
            {props.value}
        </button>
    )
}

export default RoundButton;
