import React, { Component } from "react";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import MovieLists from "./MovieLists";
import { Redirect } from "react-router-dom";
import axios from "axios";

const Profile = observer(
  class Profile extends Component {
    lists = [];

    componentDidMount() {
      this.getMovieLists();
    }
    getMovieLists = async () => {
      await axios
        .get("https://localhost:44336/api/MovieLists/Lists", {
          headers: { Authorization: `Bearer ${this.props.token}` },
        })
        .then((response) => {
          this.lists = response.data;
        });
    };

    render() {
      let listsProp = [];
      if (this.lists.length !== 0) {
        listsProp = this.lists.m_Item2;
      }
      return (
        <div>
          <Redirect to={this.props.token === "" ? "/" : "/profile"} />
          <h1>{this.props.username}</h1>
          <h2>My Movie Lists:</h2>
          <MovieLists token={this.props.token} lists={listsProp} />
        </div>
      );
    }
  }
);

decorate(Profile, {
  lists: observable,
});

export default Profile;
