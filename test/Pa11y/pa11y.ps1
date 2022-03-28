pa11y-ci --config .pa11yci.tests.portal.json --json > pa11y-ci-results.json
pa11y-ci-reporter-html

pa11y-ci --config .pa11yci.tests.streaming.manager.json --json > pa11y-ci-results.json
pa11y-ci-reporter-html
