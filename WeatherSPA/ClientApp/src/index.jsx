import 'bootstrap/dist/css/bootstrap.css';
import 'MyCSS/site.css';
import React from 'react';
import BodyForecasts from './BodyForecast';
import ReactDOM from 'react-dom';


ReactDOM.render(

    <div className="container">
        <div className="card card-2">
            <nav className="navbar navbar-dark bg-dark">
                <a className="navbar-brand" href="s">Navbar</a>
            </nav>
            <BodyForecasts />
        </div>
    </div>,

    document.getElementById('root')
);
