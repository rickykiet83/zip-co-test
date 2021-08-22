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

  });

  it('should display popup add new customer form after click', () => {
    cy.get('#add-customer-link').click();

    cy.get('.jw-modal').should('be.ok');
  });

  it('should close popup add new customer form after cancel button is clicked', () => {
    cy.get('#add-customer-link').click();
    cy.get('#btn-close-modal').click();
  });

  it('should display order of customer was selected', () => {
    cy.get('.btn-view-orders').first().click();

    cy.get('#revenue').should('be.visible');
  })


})
