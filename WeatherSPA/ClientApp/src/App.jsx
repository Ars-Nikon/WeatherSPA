import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './Layout';
import { Home } from './Home';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <div className="card card-2">
                    <Route exact path='/' component={Home} />
                    <Route path='/test'>
                        wtrwr
                    </Route>
                </div>
            </Layout>
        );
    }
}
