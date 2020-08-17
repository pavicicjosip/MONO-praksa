import React from "react";
import { Link } from "react-router-dom";

import "./SideDrawer.scss";

const sideDrawer = (props) => (
  <nav className="side-drawer">
    <ul>
      <li>
        <Link to="/movies" onClick={props.click}>
          Movies
        </Link>
      </li>
      <li>
        <a href="/">Celebs</a>
      </li>
    </ul>
  </nav>
);

export default sideDrawer;
