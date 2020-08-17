import React, { Component } from "react";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import MovieFilter from "../Movie/MovieFilter";
import axios from "axios";
import "./Profile.scss";

const CreateListForm = observer(
  class CreateListForm extends Component {
    listName = "";
    movies = [];
    handleChange = (event) => {
      this.listName = event.target.value;
    };

    handleSubmit = (event) => {
      event.preventDefault();

      this.movies.map(async (movieID) => {
        let movie = {
          ListName: this.listName,
          MovieID: movieID,
        };
        return await axios.post(
          "https://localhost:44336/api/MovieLists",
          movie,
          {
            headers: { Authorization: `Bearer ${this.props.token}` },
          }
        );
      });
      this.movies = [];

      this.props.toggle();
      this.props.getLists();
    };

    addMovie = (movieID) => {
      this.movies = this.movies.concat([movieID]);
    };

    render() {
      return (
        <form>
          <label>
            ListName:
            <input
              type="text"
              name="listName"
              value={this.listName}
              onChange={this.handleChange}
            />
          </label>
          <div className="spacer" />
          <MovieFilter
            addedMovies={this.movies}
            button={this.addMovie}
            buttonTitle="Add"
          />
          <br />
          <input
            type="submit"
            value="Create New List"
            onClick={this.handleSubmit}
          />
        </form>
      );
    }
  }
);

decorate(CreateListForm, {
  listName: observable,
  movies: observable,
});

export default CreateListForm;
