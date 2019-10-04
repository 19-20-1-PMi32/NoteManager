import * as React from 'react';
import { render } from 'react-dom';
import { Provider } from 'react-redux';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import { initializeIcons } from 'office-ui-fabric-react';
import {  options } from 'toastr';
import 'toastr/build/toastr.css';

initializeIcons();
options.timeOut = 2000;
options.positionClass = 'toast-bottom-right';