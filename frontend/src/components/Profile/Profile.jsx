import React, { Component } from "react";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import MovieLists from "./MovieLists";
import CreateListForm from "./CreateListForm";
import { Redirect } from "react-router-dom";
import axios from "axios";
import "./Profile.scss"

const Profile = observer(
  class Profile extends Component {
    lists = [];
    createFlag = false;
    pageNumber = 1;

    componentDidMount() {
      this.getMovieLists();
    }
    getMovieLists = async () => {
      await axios
        .get(
          "https://localhost:44336/api/MovieLists/Lists?pageSize=5&pageNumber=" +
            this.pageNumber,
          {
            headers: { Authorization: `Bearer ${this.props.token}` },
          }
        )
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

    handleNext = () => {
      if ((this.pageNumber + 1) * 5 < this.lists.m_Item1 + 5) {
        this.pageNumber++;
        this.getMovieLists();
      }
    };

    handlePrevious = () => {
      if (this.pageNumber !== 1) {
        this.pageNumber--;
        this.getMovieLists();
      }
    };

    render() {
      let listsProp = [];
      if (this.lists.length !== 0) {
        listsProp = this.lists.m_Item2;
      }
      return (
        <div className="container">
          <Redirect to={this.props.token === "" ? "/" : "/profile"} />
          <h2>My Movie Lists:</h2>
          <button className="create-button" onClick={this.toggleCreateForm}> Create Movie List</button>
          <MovieLists
            toggle={this.toggleMovieList}
            getLists={this.getMovieLists}
            onDelete={this.handleDelete}
            token={this.props.token}
            lists={listsProp}
            Next={this.handleNext}
            Previous={this.handlePrevious}
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
