import React, {Component} from 'react'
import {MiniCard} from './MiniCard';
import './css/CardsContentBox.css'

class CardsContentBox extends Component
{
    constructor(props)
    {
        super(props);
        this.state = {
            
        };
    }

    renderMiniCards(content_item, card_key)
    {
        return(
            <MiniCard key={`$mc-${card_key}`} mc_key={card_key} onClick={this.props.content_onClick} item={content_item}/>
        );
    }
    render()
    {
        const content = this.props.content;
        const cards = [];
        
        for(let i = 0; i < content.length; i++){
            cards.push(
                this.renderMiniCards(content[i], "MiniCard-" + i)
            );
        }

        return (
            <div className="main-content-block">
                <h2 className="main-block-title">{this.props.title}</h2>
                <div className="box-content">
                    {cards}
                </div>
            </div>
        );
    }
}

export default CardsContentBox;