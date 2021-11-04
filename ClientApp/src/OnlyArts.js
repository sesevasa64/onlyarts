import React, { Component } from 'react';
import './OnlyArts.css'
import HeaderOA from './components/HeaderOA';
import Logo from './components/Logo';
import NavigationMenu from './components/NavigationMenu';
import TagList from './components/TagList';
import CardsContentBox from './components/CardsContentBox';

class OnlyArts extends Component
{
  render () {
    return (
      <div className="main-box">
          <Logo/>
          <HeaderOA/>
          <NavigationMenu/>
          <TagList/>
          <CardsContentBox/>
      </div>
    );
  }
}

export default OnlyArts;
