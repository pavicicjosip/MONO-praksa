import React, { Component } from "react";
import MovieList from "./MovieList";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import axios from "axios";
import "./Profile.scss";

const MovieLists = observer(
  class MovieLists extends Component {
    movies = [];
    listName = "";
    pageNumber = 1;

    chooseList = async (listName) => {
      await axios
        .get(
          "https://localhost:44336/api/MovieLists/Movies?pageSize=4&pageNumber=" +
            this.pageNumber +
            "&listName=" +
            listName,
          {
            headers: { Authorization: `Bearer ${this.props.token}` },
          }
        )
        .then((response) => {
          if (response.data.m_Item1 === 0) {
            this.props.getLists();
          }
          this.movies = response.data;
          this.listName = listName;
        });
    };

    onDelete = (listName) => {
      if (this.listName === listName) {
        this.movies = [];
      }
      this.props.onDelete(listName);
    };

    handleRemove = async (movieID) => {
      await axios
        .delete(
          "https://localhost:44336/api/MovieLists?listName=" +
            this.listName +
            "&account=true&movieID=" +
            movieID,
          {
            headers: { Authorization: `Bearer ${this.props.token}` },
          }
        )
        .then((response) => {
          this.chooseList(this.listName);
        });
    };

    handleNext = () => {
      if ((this.pageNumber + 1) * 4 < this.movies.m_Item1 + 4) {
        this.pageNumber++;
        this.chooseList(this.listName);
      }
    };

    handlePrevious = () => {
      if (this.pageNumber !== 1) {
        this.pageNumber--;
        this.chooseList(this.listName);
      }
    };

    render() {
      let lists = [];
      if (this.props.lists.length !== 0) {
        lists = this.props.lists.map((list) => {
          return (
            <div key={list.ListName + "div"}>
              <button
                className="list-button"
                onClick={() => this.chooseList(list.ListName)}
                key={list.ListName}
              >
                {list.ListName}
              </button>
              <button
                className="delete-button"
                onClick={() => this.onDelete(list.ListName)}
              >
                Delete
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
          {this.props.lists.length !== 0 ? (
            <div>
              <button className="previous-button" onClick={this.props.Previous}>
                Previous
              </button>
              <button className="next-button" onClick={this.props.Next}>
                Next
              </button>
            </div>
          ) : null}
          <div className="spacer" />
          <MovieList
            Next={this.handleNext}
            Previous={this.handlePrevious}
            removeFromList={this.handleRemove}
            movies={movies}
          />
        </div>
      );
    }
  }
);

decorate(MovieLists, {
  movies: observable,
  listName: observable,
  pageNumber: observable,
});

export default MovieLists;
