import React, { Component } from "react";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import MovieLists from "./MovieLists";
import CreateListForm from "./CreateListForm";
import { Redirect } from "react-router-dom";
import axios from "axios";

const Profile = observer(
  class Profile extends Component {
    lists = [];
    createFlag = false;

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

    handleDelete = async (listName) => {
      await axios.delete(
        "https://localhost:44336/api/MovieLists?listName=" +
          listName +
          "&account=true",
        {
          headers: { Authorization: `Bearer ${this.props.token}` },
        }
      );
      this.getMovieLists();
    };

    toggleCreateForm = () => {
      this.createFlag = !this.createFlag;
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
          <button onClick={this.toggleCreateForm}> Create Movie List</button>
          <MovieLists
            onDelete={this.handleDelete}
            token={this.props.token}
            lists={listsProp}
          />
          {this.createFlag ? (
            <CreateListForm
              toggle={this.toggleCreateForm}
              getLists={this.getMovieLists}
              token={this.props.token}
            />
          ) : null}
        </div>
      );
    }
  }
);

decorate(Profile, {
  lists: observable,
  createFlag: observable,
});

export default Profile;
