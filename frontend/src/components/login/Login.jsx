import React from "react";

export class Login extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      username: "",
      password: "",
      token: "",
      items: [],
      isLoaded: false,
      url: "",
      wrong: "",
    };
  }

  handleLogin = async () => {
    let user = document.getElementById("user").value;
    let pass = document.getElementById("pass").value;
    let url =
      "https://localhost:44336/api/Account/SelectAccountAsync?userName=" +
      user +
      "&userPassword=" +
      pass;
    await fetch(url)
      .then((response) => response.json())
      .then((data) => this.setState({ token: data, wrong: "" }));
    if (this.state.token === "") {
      this.setState({ wrong: "Wrong login data" });
    }
    this.props.onLogin(this.state.token);
  };

  render() {
    console.log(this.state.token);
    return (
      <div className="base-container" ref={this.props.containerRef}>
        <div className="header">Login</div>
        <div className="content">
          <div className="image">
            <img src={require("./Person.png")} alt="" />
          </div>
          <div className="form">
            <div className="form-group">
              <p>{this.state.wrong}</p>
              <label htmlFor="username">Username</label>
              <input type="text" id="user" placeholder="username" />
            </div>
            <div className="form-group">
              <label htmlFor="password">Password</label>
              <input type="password" id="pass" placeholder="password" />
            </div>
          </div>
        </div>
        <div className="footer">
          <button type="button" className="btn" onClick={this.handleLogin}>
            Login
          </button>
        </div>
      </div>
    );
  }
}
