import React, { Component } from 'react';
import {Route, NavLink, HashRouter} from 'react-router-dom'

import './OnlyArts.css'

import HeaderOA from './components/HeaderOA';
import Logo from './components/Logo';
import NavigationMenu from './components/NavigationMenu';
import TagList from './components/TagList';
import CardsContentBox from './components/CardsContentBox';
import ContentPage from './components/ContentPage';
import {t1, t2, t3, t4} from './models/TestContentCard'

class OnlyArts extends Component
{
  constructor(props)
  {
    super(props);
    this.state = 
    {
      content_cards: [t1, t2, t3, t4],
      current_content: 0,
    }
  }

  render () {
    const current_contentPage = new ContentPage();

    return (
      <div className="main-box">
        <HashRouter>
          <Logo/>
          <HeaderOA/>
          <NavigationMenu/>
          <TagList/>
          <Route path="/ContentPage/" render={() => <ContentPage item={this.state.content_cards[this.state.current_content]}/>}/>
          <Route exact path="/" render={() => <CardsContentBox content={this.state.content_cards}/>}/>
        </HashRouter>
      </div>
    );
  }
}

/*
"https://i.ytimg.com/vi/4lifQfeZo5c/maxresdefault.jpg", 
                       "https://cdn-ru0.puzzlegarage.com/img/puzzle/5/5765_preview_r.v1.jpg",
                       "https://avatars.mds.yandex.net/get-zen_doc/1880741/pub_60ebc44a0f1e1b2a8ceb58e7_60ebc559b56ded7a70c250ed/scale_1200"],
*/
export default OnlyArts;
