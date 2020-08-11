import React from 'react';

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
        }
    }
    

    
    evaluateData() {
        var user = document.getElementById("user").value;
        var pass = document.getElementById("pass").value;
        console.log(user);
        console.log(pass);

        this.url = "https://localhost:44336/api/Account/SelectAccountAsync?userName="+ user +"&userPassword="+ pass;
        fetch(this.url)
        .then(response => response.json())
        .then(data => this.setState({token: data}));
    }
   


    render() {
        return (
            <div className="base-container" ref={this.props.containerRef}>
                <div className="header">Login</div>
                <div className="content">
                    <div className="image">
                        <img src={require('./Person.png')} alt=""/>
                    </div>
                    <div className="form">
                        <div className="form-group">
                            <label htmlFor="username">Username</label>
                            <input type="text" id="user" placeholder="username"/>
                        </div>
                        <div className="form-group">
                            <label htmlFor="password">Password</label>
                            <input type="password" id="pass" placeholder="password"/>
                        </div>
                    </div>
                </div>
                <div className="footer">
                    <button type="button" className="btn" onClick={this.evaluateData.bind(this)}>Login</button>
                </div>
            </div>
        );
    }

}

