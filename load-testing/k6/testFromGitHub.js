import http from 'k6/http';
import { check, group, sleep, fail } from 'k6';


export default () => {

  let response = http.get(`https://owp-appservice.azurewebsites.net/`);

  sleep(2);

  response = http.get(`https://owp-appservice.azurewebsites.net/Workitems/Index`);
  check(response, {
    'status is 200': (r) => r.status == 200,
  })


  sleep(3);
};