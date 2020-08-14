import React, { Component } from "react";
import "./MovieInfoPage.scss";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import axios from "axios";
import "./MovieInfoPage.scss";


const Comments = observer(
    class Comments extends Component {
        comments = []

        componentDidMount() {
            axios.get("https://localhost:44336/api/Review?movieID=99120BAA-5B4C-46CB-A1A2-0805930A0EE9")
            .then((response) => this.comments = response.data);
        }


    
    render(){
        let commentsProp = [];
        if (this.comments.length !== 0) {
          commentsProp = this.comments.m_Item2;
        }

    commentsProp = commentsProp.map((m) => {
        return (
            <div key={m.ReviewID}>
                <div className="user">
                    {m.Username}
                </div>
                <div className="comment">
                    {m.Comment}
                </div>  
            </div>
        );} );

        return(
        <div>{commentsProp}</div>
        );
    }
}
);

decorate(Comments, {
    comments: observable,
})

export default Comments;