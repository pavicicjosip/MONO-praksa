import React from "react";

import DrawerToggleButton from "../SideDrawer/DrawerToggleButton";
import "./Toolbar.scss";
import { Link } from "react-router-dom";

const toolbar = (props) => (
  <header className="toolbar">
    <nav className="toolbar__navigation">
      <div>
        <DrawerToggleButton click={props.drawerClickHandler} />
      </div>
      <div className="toolbar__logo">
        <a href="/">TMDb</a>
      </div>
      <div className="spacer" />
      <div className="toolbar_navigation-items">
        <Link to="/login">Login</Link>
      </div>
    </nav>
  </header>
);
export default toolbar;
