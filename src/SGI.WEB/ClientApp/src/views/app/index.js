import React, { Component, Suspense } from 'react';
import { Route, withRouter, Switch, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';

import AppLayout from '../../layout/AppLayout';

const Gogo = React.lazy(() =>
  import(/* webpackChunkName: "viwes-gogo" */ './gogo')
);
const SecondMenu = React.lazy(() =>
  import(/* webpackChunkName: "viwes-second-menu" */ './second-menu')
);
const BlankPage = React.lazy(() =>
  import(/* webpackChunkName: "viwes-blank-page" */ './blank-page')
);

const RolesPage = React.lazy(() =>
  import(/* webpackChunkName: "viwes-blank-page" */ './roles-page')
);

const UsersPage = React.lazy(() =>
  import(/* webpackChunkName: "viwes-blank-page" */ './users-page')
);

const UserPage = React.lazy(() =>
  import(/* webpackChunkName: "viwes-blank-page" */ './user-page')
);

class App extends Component {
  render() {
    const { match } = this.props;

    return (
      <AppLayout>
        <div className="dashboard-wrapper">
          <Suspense fallback={<div className="loading" />}>
            <Switch>
              <Redirect exact from={`${match.url}/`} to={`${match.url}/gogo`} />
              <Route
                path={`${match.url}/gogo`}
                render={props => <Gogo {...props} />}
              />
              <Route
                path={`${match.url}/second-menu`}
                render={props => <SecondMenu {...props} />}
              />
              <Route
                path={`${match.url}/blank-page`}
                render={props => <BlankPage {...props} />}
              />
              <Route
                path={`${match.url}/roles-page`}
                render={props => <RolesPage {...props} />}
              />
              <Route
                path={`${match.url}/users-page`}
                render={props => <UsersPage {...props} />}
              />
              <Route
                path={`${match.url}/user-page/:id`}
                render={props => <UserPage {...props} />}
              />
              <Redirect to="/error" />
            </Switch>
          </Suspense>
        </div>
      </AppLayout>
    );
  }
}
const mapStateToProps = ({ menu }) => {
  const { containerClassnames } = menu;
  return { containerClassnames };
};

export default withRouter(
  connect(
    mapStateToProps,
    {}
  )(App)
);
