import React, { Suspense } from 'react';
import { Redirect, Route, Switch } from 'react-router-dom';

const Users = React.lazy(() =>
  import(/* webpackChunkName: "users" */ './users')
);
const UsersMenu = ({ match }) => (
  <Suspense fallback={<div className="loading" />}>
    <Switch>
      <Redirect exact from={`${match.url}/`} to={`${match.url}/users`} />
      <Route
        path={`${match.url}/users`}
        render={props => <Users {...props} />}
      />
      <Redirect to="/error" />
    </Switch>
  </Suspense>
);
export default UsersMenu;
