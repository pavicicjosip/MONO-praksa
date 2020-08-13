import React, { Component } from "react";

class Profile extends Component {
  render() {
    return (
      <div>
        <p>{this.props.username}</p>
      </div>
    );
  }
}

export default Profile;
