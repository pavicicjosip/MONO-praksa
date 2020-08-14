import React from "react";
import axios from "axios";
import { Redirect } from "react-router-dom";

export class Register extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      url: "",
      token: "",
    };
  }

  evaluateData = async () => {
    let user = document.getElementById("user").value;
    let pass = document.getElementById("pass").value;
    var newAccount = {
      Email: document.getElementById("email").value,
      UserName: user,
      UserPassword: pass,
      FileID: "1180C2F6-A482-49F1-9628-5CA3D7EA6A3B",
    };
    console.log(newAccount);

    await axios
      .post(
        "https://localhost:44336/api/Account/InsertAccountAsync",
        newAccount
      )
      .then((response) => {
        console.log(response);
      });

    let url =
      "https://localhost:44336/api/Account/SelectAccountAsync?userName=" +
      user +
      "&userPassword=" +
      pass;
    await fetch(url)
      .then((response) => response.json())
      .then((data) => this.setState({ token: data }));
    this.props.onLogin(this.state.token, user);
  };

  render() {
    return (
      <div className="base-container" ref={this.props.containerRef}>
        <div className="header">Register</div>
        <div className="content">
          <div className="image">
            <img src={require("./person4.png")} alt="" />
          </div>
          <div className="form">
            <div className="form-group">
              <label htmlFor="username">Username</label>
              <input
                type="text"
                id="user"
                placeholder="username"
                required
              ></input>
            </div>
            <div className="form-group">
              <label htmlFor="email">Email</label>
              <input type="text" id="email" placeholder="email" />
            </div>
            <div className="form-group">
              <label htmlFor="password">Password</label>
              <input type="password" id="pass" placeholder="password" />
            </div>
          </div>
        </div>
        <div className="footer">
          <button type="button" className="btn" onClick={this.evaluateData}>
            Register
          </button>
          <Redirect to={this.state.token === "" ? "/login" : "/"} />
        </div>
      </div>
    );
  }
}
