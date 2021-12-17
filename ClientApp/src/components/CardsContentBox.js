import React, {Component} from 'react'
import {MiniCard} from './MiniCard';
import './css/CardsContentBox.css'
import RoundButton from './RoundButton';
import LoadingPage from './LoadingPage';



class CardsContentBox extends Component
{
    constructor(props)
    {
        super(props);
        this.state = {
            contentIsSuccessfulLoad: true,
            contentIsLoading: false,
        };
    }

    renderButtons(Current_Number, N)
    {
        let items = [];
        let min = 0;
        let max = 18;
        for(let i = Current_Number - 2; i < Current_Number + N; i++)
        {
            if(i < 0)
            {
                continue;
            }
            console.log(i);
            items.push(
                <RoundButton className="down-buttons-list" value={`${i+1}`} onClick={() => {
                    this.setState({
                        contentIsLoading: true
                    })
                    this.props.loadContent((max - min + 1) * i , max * (i + 1),
                    (loadCorrect)=>{
                        console.log(true);
                        this.setState({
                            contentIsLoading: false,
                            contentIsSuccessfulLoad: loadCorrect,
                        })
                    })
                }}/>
            )
        }
        return items;
    }

    renderMiniCards(content_item, card_key)
    {
        return(
            <MiniCard key={`$mc-${card_key}`} mc_key={card_key} onClick={this.props.content_onClick} item={content_item}/>
        );
    }
    render()
    {
        if(this.state.contentIsLoading)
        {
            return(
                <div className="main-content-block">
                    <LoadingPage/>
                </div>
            )
        }
        else
        {
            const content = this.props.content;
            const cards = [];
            for(let i = 0; i < content.length; i++){
                cards.push(
                    this.renderMiniCards(content[i], "MiniCard-" + i)
                );
            }
            let buttons;
            if(this.props.currentPageNumber)
            {
                buttons = this.renderButtons(this.props.currentPageNumber, 2);
            }
            else
            {
                buttons = this.renderButtons(0, 2);
            }
            return (
                <div className="main-content-block">
                    <h2 className="main-block-title">{this.props.title}</h2>
                    <div className="box-content">
                        {cards}
                    </div>
                    <div className="bottom-buttons">
                            {buttons} ...
                    </div>
                </div>
            );
        }
    }
}

export default CardsContentBox;