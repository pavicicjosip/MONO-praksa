import React from "react";

import DrawerToggleButton from "../SideDrawer/DrawerToggleButton";
import "./Toolbar.scss";
import { Link } from "react-router-dom";

const toolbar = (props) => (
  <header className="toolbar">
    <nav className="toolbar__navigation">
      
      <div className="toolbar__logo">
        <a href="/"><img src="https://thetvdb.com/images/logo.png" alt="" /></a>
      </div>
      <div>
        <DrawerToggleButton click={props.drawerClickHandler} />
      </div>
      <div className="spacer" />
      <div className="toolbar_navigation-items">
        <Link to="/login">LOGIN</Link>
      </div>
    </nav>
  </header>
);
export default toolbar;
