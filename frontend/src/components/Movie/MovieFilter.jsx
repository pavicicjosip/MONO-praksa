import React, { Component } from "react";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import axios from "axios";
import Movies from "./Movies";

const MovieFilter = observer(
  class MovieFilter extends Component {
    genres = [];
    movies = [];
    pageNumber = 1;
    title = "default";
    year = "default";
    genre = "default";
    column = "default";
    order = true;
    componentDidMount() {
      axios
        .get("https://localhost:44336/api/Genre/getAllGenres")
        .then((response) => {
          this.genres = response.data;
        });
    }
    handleInputChange = (event) => {
      const target = event.target;
      if (target.name === "title") {
        this.title = target.value;
      } else if (target.name === "year") {
        this.year = target.value;
      } else if (target.name === "genre") {
        this.genre = target.value;
      } else if (target.name === "column") {
        this.column = target.value;
      } else {
        this.order = target.checked;
      }
    };

    filterEvent = async (event) => {
      event.preventDefault();
      this.filter();
    };

    filter = async () => {
      this.title = this.title === "" ? "default" : this.title;
      this.year = this.year === "" ? "default" : this.year;
      let flags = `&title=${this.title}&yearOfProduction=${this.year}&genre=${this.genre}&column=${this.column}&order=${this.order}`;

      await axios
        .get(
          "https://localhost:44336/api/Movie?pageSize=4&pageNumber=" +
            this.pageNumber +
            flags
        )
        .then((response) => {
          this.movies = response.data;
        });
    };

    Next = (event) => {
      event.preventDefault();
      if ((this.pageNumber + 1) * 4 < this.movies.m_Item1 + 4) {
        this.pageNumber++;
        this.filter();
      }
    };

    Previous = (event) => {
      event.preventDefault();
      if (this.pageNumber !== 1) {
        this.pageNumber--;
        this.filter();
      }
    };

    render() {
      let genresToSelect = [];
      let moviesProp = [];
      if (this.genres.length !== 0) {
        genresToSelect = this.genres.map((genre) => {
          return (
            <option key={genre.GenreID} value={genre.Title}>
              {genre.Title}
            </option>
          );
        });
      }
      if (this.movies.length !== 0) {
        moviesProp = this.movies.m_Item2;
      }
      return (
        <div>
          <div>
            <label>
              Title
              <input
                name="title"
                type="text"
                onChange={this.handleInputChange}
              />
            </label>
            <label>
              Year Of Production
              <input
                name="year"
                type="text"
                onChange={this.handleInputChange}
              />
            </label>
            <label>
              Genre
              <select
                name="genre"
                value={this.genre}
                onChange={this.handleInputChange}
              >
                <option value="default">All</option>
                {genresToSelect}
              </select>
            </label>
            <label>
              Order By
              <select
                name="column"
                value={this.column}
                onChange={this.handleInputChange}
              >
                <option value="Title">Title</option>
                <option value="YearOfProduction">Year of production</option>
                <option value="CountryOfOrigin">Country of origin</option>
                <option value="Duration">Duration</option>
                <option value="COUNT(ReviewID)">Most reviewed</option>
                <option value="NumberOfStars">Rating</option>
              </select>
            </label>
            <label>
              Ascending/Descending
              <input
                type="checkbox"
                checked={this.order}
                onChange={this.handleInputChange}
              />
            </label>
            <button onClick={this.filterEvent}>Filter</button>
          </div>
          <Movies
            button={this.props.button}
            buttonTitle={this.props.buttonTitle}
            movies={moviesProp}
          />
          {this.movies.length !== 0 ? (
            <div>
              <button onClick={this.Next}>Next</button>
              <button onClick={this.Previous}>Previous</button>
            </div>
          ) : null}
        </div>
      );
    }
  }
);

decorate(MovieFilter, {
  genres: observable,
  movies: observable,
  pageNumber: observable,
  title: observable,
  year: observable,
  genre: observable,
  column: observable,
  order: observable,
});

export default MovieFilter;
