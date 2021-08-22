describe('Customer List Page', () => {
  beforeEach(() => {
    cy.fixture('customers.json').as('customersJSON');

    cy.server();

    cy.route('api/customersservice/customers', '@customersJSON').as('customers');

    cy.visit('/customers/list');
  });

  it('should display a list of customers', () => {

    cy.contains("Customer List");

    cy.wait('@customers');

    cy.get('.row.customer-list').should('have.lengthOf.at.least', 29);

  })
})
