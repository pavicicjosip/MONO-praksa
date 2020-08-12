import React, { Component } from "react";

import { LoginPage } from "./components/login/Index";
import Toolbar from "./components/Toolbar/Toolbar";
import SideDrawer from "./components/SideDrawer/SideDrawer";
import Backdrop from "./components/Backdrop/Backdrop";
import HomePage from "./components/HomePage/HomePage";
import { Switch, Route } from "react-router-dom";

class App extends Component {
  state = {
    SideDrawerOpen: false,
    token: "",
  };

  drawerToggleClickHandler = () => {
    this.setState((prevState) => {
      return { SideDrawerOpen: !prevState.SideDrawerOpen };
    });
  };

  backdropClickHandler = () => {
    this.setState({ SideDrawerOpen: false });
  };

  evaluateData = (token) => {
    this.setState({ token: token });
  };

  render() {
    let sideDrawer;
    let backdrop;

    if (this.state.SideDrawerOpen) {
      sideDrawer = <SideDrawer />;
      backdrop = <Backdrop click={this.backdropClickHandler} />;
    }
    return (
      <div style={{ height: "100%" }}>
        <Toolbar drawerClickHandler={this.drawerToggleClickHandler} />
        {sideDrawer}
        {backdrop}
        <main style={{ marginTop: "64px" }}>
          <Switch>
            <Route path="/login">
              <LoginPage onLogin={this.evaluateData} />
            </Route>
            <Route path="/">
              <HomePage />
            </Route>
          </Switch>
          <p>{this.state.token}</p>
        </main>
      </div>
    );
  }
}
export default App;
