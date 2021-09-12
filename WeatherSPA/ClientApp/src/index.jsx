import React from 'react';
import Forecast from './forecasts';
import ReactDOM from 'react-dom';



class Forecasts extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            error: null,
            isLoaded: false,
            forecastNow: { date: new Date() },
            forecastForDays: []
        };
    }

    componentDidMount() {
        fetch("/api/Forecasts/Forecasts/")
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        isLoaded: true,
                        forecastNow: result.forecastNow,
                        forecastForDays: result.forecastForDays
                    });
                },
                (error) => {
                    this.setState({
                        isLoaded: true,
                        error
                    });
                }
            )
    }
    render() {

        const { error, isLoaded, forecastNow, forecastForDays } = this.state;
        if (error) {
            return <div>Ошибка {error.message}</div>;
        } else if (!isLoaded) {
            return <div>Загрузка....</div>;
        } else {
            return (
                <div className="card card-2">                 
                    <Forecast ForecastNow={forecastNow} ForecastForDays = {forecastForDays} />
                </div>
            );
        }
    }
}

ReactDOM.render(

    <Forecasts />,
    document.getElementById('root')
);
