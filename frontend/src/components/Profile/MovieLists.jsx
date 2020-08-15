import React, { Component } from "react";
import MovieList from "./MovieList";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import axios from "axios";

const MovieLists = observer(
  class MovieLists extends Component {
    movies = [];

    chooseList = async (listName) => {
      await axios
        .get(
          "https://localhost:44336/api/MovieLists/Movies?listName=" + listName,
          {
            headers: { Authorization: `Bearer ${this.props.token}` },
          }
        )
        .then((response) => {
          this.movies = response.data;
        });
    };

    render() {
      let lists = [];
      if (this.props.lists.length !== 0) {
        lists = this.props.lists.map((list) => {
          return (
            <div key={list.ListName + "div"}>
              <button
                onClick={() => this.chooseList(list.ListName)}
                key={list.ListName}
              >
                {list.ListName}
              </button>
            </div>
          );
        });
      }
      let movies = [];
      if (this.movies.length !== 0) {
        movies = this.movies.m_Item2;
      }
      return (
        <div>
          {lists}
          <MovieList movies={movies} />
        </div>
      );
    }
  }
);

decorate(MovieLists, {
  movies: observable,
});

export default MovieLists;
