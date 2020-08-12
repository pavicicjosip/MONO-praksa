import React from "react";
import axios from "axios";

export class Register extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      url: "",
    };
  }

  evaluateData() {
    var newAccount = {
      Email: document.getElementById("email").value,
      UserName: document.getElementById("user").value,
      UserPassword: document.getElementById("pass").value,
      FileID: "1180C2F6-A482-49F1-9628-5CA3D7EA6A3B",
    };
    console.log(newAccount);

    axios
      .post(
        "https://localhost:44336/api/Account/InsertAccountAsync",
        newAccount
      )
      .then((response) => {
        console.log(response);
      });
  }

  render() {
    return (
      <div className="base-container" ref={this.props.containerRef}>
        <div className="header">Register</div>
        <div className="content">
          <div className="image">
            <img src={require("./Person.png")} alt="" />
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
          <button
            type="button"
            className="btn"
            onClick={this.evaluateData.bind(this)}
          >
            Register
          </button>
        </div>
      </div>
    );
  }
}
