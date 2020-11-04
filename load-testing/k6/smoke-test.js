import http from 'k6/http';
import { check, group, sleep, fail } from 'k6';

export let options = {
  vus: 1, // 1 user looping for 1 minute
  duration: '1m',

  thresholds: {
    http_req_duration: ['p(99)<1500'], // 99% of requests must complete below 1.5s
  },
};
 

export default () => {

  let response = http.get(`https://owp-appservice.azurewebsites.net/`);

  sleep(1);

  response = http.get(`https://owp-appservice.azurewebsites.net/Workitems/Index`);
  check(response, {
    'status is 200': (r) => r.status == 200,
  })


  sleep(1);
};
