import React from 'react';


function svgLink(icon) {
    return "https://yastatic.net/weather/i/icons/funky/dark/" + icon + ".svg";
}

function Degreesformat(deg) {
    if (deg > 0) {
        return "+" + deg + "°"
    }
    else {
        return "-" + deg + "°"
    }
}

const DayOfWeek = new Map(
    [
        ["понедельник", "Пн"],
        ["вторник", "Вт"],
        ["среда", "Ср"],
        ["четверг", "Чт"],
        ["пятница", "Пт"],
        ["суббота", "Сб"],
        ["воскресенье", "Вс"],
    ]);

class Forecast extends React.Component {
    render() {
        return (
            <div>
                <ForecastNow forecastNow={this.props.ForecastNow} />
                <ForecastForDay forecastForDays={this.props.ForecastForDays} />
                <ForecastForHour />
            </div>
        );
    };
}


class ForecastNow extends React.Component {
    options = {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        weekday: 'long',
        timezone: 'UTC',
        hour: 'numeric',
        minute: 'numeric',
    };



    condition = new Map(
        [
            ["clear", "ясно"],
            ["partly-cloudy", "малооблачно"],
            ["cloudy", "облачно с прояснениями"],
            ["overcast", "пасмурно"],
            ["drizzle", "морось"],
            ["light-rain", "небольшой дождь"],
            ["rain", "дождь"],
            ["moderate-rain", "умеренно сильный дождь"],
            ["heavy-rain", "сильный дождь"],
            ["continuous-heavy-rain", " длительный сильный дождь"],
            ["showers", "ливень"],
            ["wet-snow", "дождь со снегом"],
            ["light-snow", "небольшой снег"],
            ["snow", "снег"],
            ["snow-showers", "снегопад"],
            ["hail", "град"],
            ["thunderstorm", "гроза"],
            ["thunderstorm-with-rain", "дождь с грозой"],
            ["thunderstorm-with-hail", "гроза с градом"],
        ]);

    render() {
        return (
            <div className="card card-2" >
                <div className="row ">
                    <div className="carousel-inner">
                        <div className="carousel-item active">
                            <div className="row">
                                <div className="col-6">
                                    <div className="location">{this.props.forecastNow.city}</div>
                                    <div className="date">{new Date(this.props.forecastNow.date).toLocaleString("ru", this.options)}</div>
                                    <div className="temp">{Degreesformat(this.props.forecastNow.nowTemp)}</div>
                                    <div className="conditionSky">{this.condition.get(this.props.forecastNow.conditionSky)}</div>
                                    <div>
                                        <span className="faicon">
                                            <i className="fas fa-wind "></i> {this.props.forecastNow.windSpeed} М/С,
                                        </span  >

                                        <span className="faicon">
                                            <i className=" fas fa-compass "></i> {this.props.forecastNow.directWind},
                                        </span>

                                        <span className="faicon">
                                            <i className=" fas fa-umbrella "></i> {this.props.forecastNow.precProb}%
                                        </span>
                                    </div>
                                </div>
                                <div className="col-6 justify-content-right"><img alt="" className="img-fluid" src={svgLink(this.props.forecastNow.icon)} /></div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        );
    }
}


class ForecastForDay extends React.Component {
    constructor(props) {
        super(props);
    };

    render() {
        return (
            <div className="card card-3">
                <div className="row">
                    {this.props.forecastForDays.map(day => (
                        
                        <Day key={day.date} forecastDay={day} />
                    ))}
                </div>
            </div>
        );
    };

}

function Day(props) {
    return (
        <div className="col">
            <div className="row daterow">{DayOfWeek.get(new Date(props.forecastDay.date).toLocaleString("ru", { weekday: 'long' }))}   </div>
            <div className="row daterowday">{new Date(props.forecastDay.date).toLocaleString("ru", {
                month: 'long',
                day: 'numeric'
            })}
            </div>
            <div className="row temprow">{Degreesformat(props.forecastDay.maxTemp)}</div>
            <div className="row row2"><img className="img-fluid" alt="" src={svgLink(props.forecastDay.icon)} /></div>
            <div className="row temprow">{Degreesformat(props.forecastDay.minTemp)}</div>
            <div className="test">
                <div className="row  faicon ">
                    <i className="fas fa-wind "> {props.forecastDay.windSpeed} М/С</i>
                </div>
                <div className="row faicon  ">
                    <i className=" fas fa-compass "></i> {props.forecastDay.directWind}
                </div>
                <div className="row faicon  ">
                    <i className=" fas fa-umbrella "> {props.forecastDay.precProb}%</i>
                </div>
            </div>
        </div>
    );
}



class ForecastForHour extends React.Component {
    render() {
        return (
            <div className="card card-2" >
                <div className="carousel slide" data-ride="carousel">
                    <div className="carousel-inner">
                        <div className="carousel-item active">
                            <div className="row">
                                <div className="col">
                                    <div className="row row1">21&deg;</div>
                                    <div className="row row2"><img alt="" className="img-fluid" src="https://img.icons8.com/ios/100/000000/sun.png" /></div>
                                    <div className="row row3">12:00</div>
                                    <div className="row row4">PM</div>
                                </div>
                                <div className="col">
                                    <div className="row row1">21&deg;</div>
                                    <div className="row row2"><img alt="" className="img-fluid" src="https://img.icons8.com/ios/100/000000/sun.png" /></div>
                                    <div className="row row3">12:00</div>
                                    <div className="row row4">PM</div>
                                </div>
                                <div className="col">
                                    <div className="row row1">21&deg;</div>
                                    <div className="row row2"><img alt="" className="img-fluid" src="https://img.icons8.com/ios/100/000000/sun.png" /></div>
                                    <div className="row row3">12:00</div>
                                    <div className="row row4">PM</div>
                                </div>
                                <div className="col">
                                    <div className="row row1">21&deg;</div>
                                    <div className="row row2"><img alt="" className="img-fluid" src="https://img.icons8.com/ios/100/000000/sun.png" /></div>
                                    <div className="row row3">12:00</div>
                                    <div className="row row4">PM</div>
                                </div>
                                <div className="col">
                                    <div className="row row1">21&deg;</div>
                                    <div className="row row2"><img alt="" className="img-fluid" src="https://img.icons8.com/ios/100/000000/sun.png" /></div>
                                    <div className="row row3">12:00</div>
                                    <div className="row row4">PM</div>
                                </div>
                                <div className="col">
                                    <div className="row row1">21&deg;</div>
                                    <div className="row row2"><img alt="" className="img-fluid" src="https://img.icons8.com/ios/100/000000/sun.png" /></div>
                                    <div className="row row3">12:00</div>
                                    <div className="row row4">PM</div>
                                </div>
                                <div className="col">
                                    <div className="row row1">21&deg;</div>
                                    <div className="row row2"><img alt="" className="img-fluid" src="https://img.icons8.com/ios/100/000000/sun.png" /></div>
                                    <div className="row row3">12:00</div>
                                    <div className="row row4">PM</div>
                                </div>
                            </div>
                            <div className="row">
                                <div className="col">
                                    <div className="row row1">21&deg;</div>
                                    <div className="row row2"><img alt="" className="img-fluid" src="https://img.icons8.com/ios/100/000000/sun.png" /></div>
                                    <div className="row row3">12:00</div>
                                    <div className="row row4">PM</div>
                                </div>
                                <div className="col">
                                    <div className="row row1">21&deg;</div>
                                    <div className="row row2"><img alt="" className="img-fluid" src="https://img.icons8.com/ios/100/000000/sun.png" /></div>
                                    <div className="row row3">12:00</div>
                                    <div className="row row4">PM</div>
                                </div>
                                <div className="col">
                                    <div className="row row1">21&deg;</div>
                                    <div className="row row2"><img alt="" className="img-fluid" src="https://img.icons8.com/ios/100/000000/sun.png" /></div>
                                    <div className="row row3">12:00</div>
                                    <div className="row row4">PM</div>
                                </div>
                                <div className="col">
                                    <div className="row row1">21&deg;</div>
                                    <div className="row row2"><img alt="" className="img-fluid" src="https://img.icons8.com/ios/100/000000/sun.png" /></div>
                                    <div className="row row3">12:00</div>
                                    <div className="row row4">PM</div>
                                </div>
                                <div className="col">
                                    <div className="row row1">21&deg;</div>
                                    <div className="row row2"><img alt="" className="img-fluid" src="https://img.icons8.com/ios/100/000000/sun.png" /></div>
                                    <div className="row row3">12:00</div>
                                    <div className="row row4">PM</div>
                                </div>
                                <div className="col">
                                    <div className="row row1">21&deg;</div>
                                    <div className="row row2"><img alt="" className="img-fluid" src="https://img.icons8.com/ios/100/000000/sun.png" /></div>
                                    <div className="row row3">12:00</div>
                                    <div className="row row4">PM</div>
                                </div>
                                <div className="col">
                                    <div className="row row1">21&deg;</div>
                                    <div className="row row2"><img alt="" className="img-fluid" src="https://img.icons8.com/ios/100/000000/sun.png" /></div>
                                    <div className="row row3">12:00</div>
                                    <div className="row row4">PM</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    };
}



export default Forecast;