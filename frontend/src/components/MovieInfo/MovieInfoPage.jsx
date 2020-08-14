import React, { Component } from "react";
import "./MovieInfoPage.scss";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import axios from "axios";
import Movie from "../Movie/Movie";
import Comments from "./Comments";

const MovieInfoPage = observer(
    class MovieInfoPage extends Component {
        movie = { FileID: "1180C2F6-A482-49F1-9628-5CA3D7EA6A3B" }


        componentDidMount() {
            axios.get("https://localhost:44336/api/Movie/" + this.props.match.params.id)
            .then((response) => this.movie = response.data);
        }

        postComment()
        {
            let comm = document.getElementById("commentToPost").value;

            var newComment = {
                NumberOfStars: document.getElementById("email").value,
                UserName: document.getElementById("user").value,
                UserPassword: document.getElementById("pass").value,
                FileID: "1180C2F6-A482-49F1-9628-5CA3D7EA6A3B",
            };
              console.log(newAccount);
          

            console.log(comm)
        }

        render() {
            let moviesProp = [];
            if (this.movie.length !== 0) {
                moviesProp = this.movie;
            }

            return(
                <div>
                    <div  className="base-container">

                        <div className="image">
                            <Movie key={this.movie.MovieID} movie={moviesProp} />
                        </div>  

                        
                    </div>

                    <div className="info">
                        <div>
                                Title: {this.movie.Title}
                            </div>
                            <div>
                                Year of production: {this.movie.YearOfProduction}
                            </div>
                            <div>
                                Country of origin: {this.movie.CountryOfOrigin}
                            </div>
                            <div>
                                Duration: {this.movie.Duration} minutes
                            </div>
                            <div>
                                Rating: {this.movie.Rating} / 10
                            </div>
                            <div></div>
                            <div></div>
                            
                    </div>

                    <div className="color">
                                Comments
                    </div>

                    <div>
                    <textarea id="commentToPost" name="Text1" className="input" maxLength="999" rows="6"></textarea>
                    </div>
                    <button type="button" className="postBtn" onClick={this.postComment}>
                        Post
                    </button>

                    <div className="base-comment">
                        <Comments />
                    </div>
                    
                
                    
                </div>
               
            );

            
        }
    }
);

decorate(MovieInfoPage, {
    movie: observable,
});

export default MovieInfoPage;
