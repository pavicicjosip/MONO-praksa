import React from 'react';

import './SideDrawer.scss';

const sideDrawer = props => (
    <nav className="side-drawer">
        <ul>
            <li><a href="/">Movies</a></li>
            <li><a href="/">Celebs</a></li>

        </ul>
    </nav>
);

export default sideDrawer;