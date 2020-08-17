import React, { Component } from "react";

import { LoginPage } from "./components/login/Index";
import Toolbar from "./components/Toolbar/Toolbar";
import SideDrawer from "./components/SideDrawer/SideDrawer";
import Backdrop from "./components/Backdrop/Backdrop";
import HomePage from "./components/HomePage/HomePage";
import { Switch, Route } from "react-router-dom";
import MovieInfoPage from "./components/MovieInfo/MovieInfoPage";
import Profile from "./components/Profile/Profile";

class App extends Component {
  state = {
    SideDrawerOpen: false,
    token: "",
    user: "LOGIN",
    path: "",
  };

  drawerToggleClickHandler = () => {
    this.setState((prevState) => {
      return { SideDrawerOpen: !prevState.SideDrawerOpen };
    });
  };

  backdropClickHandler = () => {
    this.setState({ SideDrawerOpen: false });
  };

  evaluateData = (token, user) => {
    this.setState({ token: token, user: user });
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
      <Toolbar
          username={this.state.user}
          token={this.state.token}
          drawerClickHandler={this.drawerToggleClickHandler}
        />
        {sideDrawer}
        {backdrop}
        <main style={{ marginTop: "64px" }}>
          <Switch>
            <Route path="/login">
              <LoginPage onLogin={this.evaluateData} />
            </Route>
            <Route
              exact
              path="/movieInfoPage/:id"
              render={(props) => <MovieInfoPage {...props} token={this.state.token} />}
            />
            <Route path="/profile">
              <Profile token={this.state.token} username={this.state.user} />
            </Route>
            <Route path="/">
              <HomePage />
            </Route>
          </Switch>
        </main>
      </div>
    );
  }
}
export default App;
