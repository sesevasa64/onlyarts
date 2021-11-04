import React, {Component} from 'react'
import MiniCard from './MiniCard';
import './CardsContentBox.css'

class CardsContentBox extends Component
{
    constructor(props)
    {
        super(props);
        this.state = {
            content: ["https://i.ytimg.com/vi/4lifQfeZo5c/maxresdefault.jpg", 
                       "https://cdn-ru0.puzzlegarage.com/img/puzzle/5/5765_preview_r.v1.jpg",
                       "https://avatars.mds.yandex.net/get-zen_doc/1880741/pub_60ebc44a0f1e1b2a8ceb58e7_60ebc559b56ded7a70c250ed/scale_1200"],
        };
    }

    renderMiniCards(content)
    {
        return(
            <MiniCard src={content}/>
        );
    }
    render()
    {
        const content = this.state.content;
        const cards = [];
        
        for(let i = 0; i < content.length; i++){
            cards.push(
                this.renderMiniCards(content[i])
            );
        }

        return (
            <div className="box-content">
                {cards}
            </div>
        );
    }
}

export default CardsContentBox;